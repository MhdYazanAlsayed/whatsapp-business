using Microsoft.Extensions.Logging;
using SprintBuisness.Whatapp;
using SprintBusiness.Domain.Templates;
using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;
using SprintBusiness.Shared.Configurations;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Shared.Helpers;
using SprintBusiness.Whatsapp.Constant.Template;
using SprintBusiness.Whatsapp.Constant.TemplateDtos;
using SprintBusiness.Whatsapp.Dtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Create;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Send;
using SprintBusiness.Whatsapp.Validators;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SprintBusiness.Whatsapp
{
    public class WhatsappProvider : IWhatsappProvider
    {
        private readonly WhatsappApiConfiguration _configuration;
        private readonly ApiSettingsConfiguration _settings;
        private readonly ILogger<WhatsappProvider> _logger;

        public WhatsappProvider(WhatsappApiConfiguration configuration,
            ApiSettingsConfiguration settings,
            ILogger<WhatsappProvider> logger)
        {
            _configuration = configuration;
            _settings = settings;
            _logger = logger;
        }

        public async Task<ResultDto> SendTextMessageAsync(SendMessageDto dto)
        {
            if (ApiEnvironment.IsDevelopment(_settings.Environment))
                return ResultDto.Success();

            _logger.LogInformation("Sending message to {0} at {1}", dto.To, DateTimeCulture.Now);

            var request = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = dto.To,
                type = "text",
                text = new
                {
                    preview_url = false,
                    body = dto.Message
                }
            };

            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                _configuration.Api + _configuration.PhoneNumberId + "/messages");

            requestMessage.Headers.Add(
                "Authorization",
                $"Bearer {_configuration.AccessToken}");

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");
            requestMessage.Content = content;

            using var httpClient = new HttpClient();

            var response = await httpClient.SendAsync(requestMessage);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();

                _logger.LogError("Send text message faild with response :");
                _logger.LogError(responseAsString);

                return ResultDto.Failure();
            }

            _logger.LogInformation(
                "Message successfully sent to {PhoneNumber} at {Time}",
                dto.To,
                DateTimeCulture.Now);

            return ResultDto.Success();
        }

        public async Task<ResultDto> SendReplayButtonMessageAsync(SendInteractiveMessageDto dto)
        {
            if (ApiEnvironment.IsDevelopment(_settings.Environment))
                return ResultDto.Success();

            var request = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = dto.To,
                type = "interactive",
                interactive = new
                {
                    type = "button",
                    body = new
                    {
                        text = dto.Message
                    },
                    action = new
                    {
                        buttons = dto.Buttons.Select(x => new
                        {
                            type = "reply",
                            reply = new
                            {
                                id = x.Id,
                                title = x.Text
                            }
                        })
                    }
                },
            };

            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                _configuration.Api + _configuration.PhoneNumberId + "/messages");

            requestMessage.Headers.Add(
                "Authorization",
                $"Bearer {_configuration.AccessToken}");

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");
            requestMessage.Content = content;

            using var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("Interactive message sent successfully to {to} .", dto.To);

                return ResultDto.Success();
            }

            var responseAsString = await response.Content.ReadAsStringAsync();
            _logger.LogError("Send interactive message faild with response :");
            _logger.LogError(responseAsString);

            return ResultDto.Failure();
        }

        public async Task<ResultDto> SendListMessageAsync(SendMenuMessageDto dto)
        {
            if (ApiEnvironment.IsDevelopment(_settings.Environment))
                return ResultDto.Success();

            var requestMessage = new HttpRequestMessage(
            HttpMethod.Post,
            _configuration.Api + _configuration.PhoneNumberId + "/messages");

            requestMessage.Headers.Add(
                "Authorization",
                $"Bearer {_configuration.AccessToken}");

            var content = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json");
            requestMessage.Content = content;

            using var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation("List message sent successfully to {to} .", dto.To);

                return ResultDto.Success();
            }

            var responseAsString = await response.Content.ReadAsStringAsync();
            _logger.LogError("Send list message faild with response :");
            _logger.LogError(responseAsString);

            return ResultDto.Failure();
        }

        public async Task<ResultDto> SendTemplateMessageAsync(SendTemplateMessageDto dto)
        {
            var url = $"{_configuration.Api + _configuration.PhoneNumberId}/messages";

            var requestData = new
            {
                messaging_product = "whatsapp",
                recipient_type = "individual",
                to = dto.PhoneNumber,
                type = "template",
                template = new
                {
                    name = dto.Name,
                    language = new { code = dto.Language },
                    components = dto.Components?.Select(GetComponent).ToList()
                }
            };

            var jsonRequest = JsonSerializer.Serialize(requestData);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            using var _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.AccessToken}");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Template sent successfully !");

                return ResultDto.Success();
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError(errorContent);

            return ResultDto.Failure();


            // Get the request json from dto using type of parameter
            object GetComponent(SendTemplateMessageComponent component)
            {
                var parameters = new List<object>();

                foreach (var parameter in component.Parameters)
                {
                    var type = parameter.Type.ToLower();

                    if (type == TemplateParameterTypes.Text)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.Text,
                            text = parameter.Text
                        });

                        continue;
                    }

                    if (type == TemplateParameterTypes.Currency)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.Currency,
                            currency = new
                            {
                                currency_code = parameter.Currency!.Code,
                                amount_1000 = parameter.Currency!.Amount
                            }
                        });

                        continue;
                    }

                    if (type == TemplateParameterTypes.DateTime)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.DateTime,
                            date_time = new
                            {
                                fallback_value = parameter.DateTime!.FallBack
                            }
                        });

                        continue;
                    }

                    if (type == TemplateParameterTypes.Document)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.Document,
                            document = new
                            {
                                link = parameter.Document!.Link,
                                //filename = parameter.Document!.FileName
                            }
                        });

                        continue;
                    }

                    if (type == TemplateParameterTypes.Image)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.Image,
                            image = new
                            {
                                link = parameter.Image!.Link,
                            }
                        });

                        continue;
                    }

                    if (type == TemplateParameterTypes.Video)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.Video,
                            video = new
                            {
                                link = parameter.Video!.Link,
                            }
                        });

                        continue;
                    }

                    if (type == TemplateParameterTypes.Audio)
                    {
                        parameters.Add(new
                        {
                            type = TemplateParameterTypes.Audio,
                            audio = new
                            {
                                link = parameter.Audio!.Link,
                            }
                        });

                        continue;
                    }

                    throw new NotImplementedException();
                }

                return new
                {
                    type = component.Type.ToString().ToLower(),
                    parameters = parameters
                };
            }
        }

        public async Task<ResultDto<List<Template>>> GetAllTemplatesAsync()
        {
            try
            {
                using var _httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration.Api + _configuration.AccountId}/message_templates");

                request.Headers.Add(
                    "Authorization",
                    $"Bearer {_configuration.AccessToken}");

                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error while get templates from whatsapp api .");

                    return ResultDto<List<Template>>.Failure();
                }

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Facebook API Response: {Response}", content);

                var templates = new List<Template>();

                var templatesResponse = JsonSerializer.Deserialize<WhatsAppTemplateRoot>(content);

                foreach (var item in templatesResponse!.Data)
                {
                    var template = Template
                    .Create(item.Id, item.Name, 
                        GetTemplateStatusFromResponse(item.Status),
                        GetTemplateCategoryFromResponse(item.Category), 
                        item.SubCategory, 
                        GetTemplateLanguageFromResponse(item.Language) );

                    foreach (var component in item.Components.Where(x => x.Type != "BUTTONS"))
                    {
                        var componentEntity = template.AddComponent(
                            GetComponentTypeFromResponse(component.Type) ,
                            GetComponentFormatFromResposne(component.Format) ,
                            component.Text
                        );

                        if (component.Example is not null)
                        {
                            var key = string.Join(",",
                                component.Example.HeaderHandle ?? new List<string>());

                            var value = component.Example.BodyText != null ?
                                JsonSerializer.Serialize(component.Example.BodyText) :
                                string.Empty;

                            //componentEntity.AddVariable(key, value);
                        }
                    }

                    foreach (var component in item.Components.Where(x => x.Type == "BUTTONS"))
                    {
                        foreach (var button in component.Buttons)
                        {
                            template.AddButton(button.Url, GetButtonTypeFromResponse(button.Type), button.Text);
                        }
                    }
                    templates.Add(template);
                }

                return ResultDto<List<Template>>.Success(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching templates from Facebook API");
                throw;
            }

            // Local methods

            TemplateStatus GetTemplateStatusFromResponse(string status)
            {
                return status.ToLower() switch
                {
                    "approved" => TemplateStatus.APPROVED,
                    "pending" => TemplateStatus.PENDING,
                    "rejected" => TemplateStatus.REJECTED,
                    _ => throw new NotImplementedException()
                };
            }

            TemplateCategory GetTemplateCategoryFromResponse(string category)
            {
                return category.ToLower() switch
                {
                    "utility" => TemplateCategory.Utility,
                    "marketing" => TemplateCategory.Marketing,
                    _ => throw new NotImplementedException()
                };
            }

            TemplateLanguage GetTemplateLanguageFromResponse(string language)
            {
                return language.ToLower() switch
                {
                    "en_us" => TemplateLanguage.English,
                    "ar_ar" => TemplateLanguage.Arabic,
                    _ => TemplateLanguage.Unkown 
                };
            }


            TemplateComponentType GetComponentTypeFromResponse(string type)
            {
                type = type.ToLower();

                if (type == TemplateComponentTypes.Header) 
                    return TemplateComponentType.Header;

                if (type == TemplateComponentTypes.Body)
                    return TemplateComponentType.Body;

                if (type == TemplateComponentTypes.Footer)
                    return TemplateComponentType.Footer;

                throw new NotImplementedException();
            }

            TemplateComponentFormat GetComponentFormatFromResposne (string? format)
            {
                if (format == TemplateComponentFormats.Text || format is null)
                    return TemplateComponentFormat.Text;

                if (format == TemplateComponentFormats.Document)
                    return TemplateComponentFormat.Document;

                if (format == TemplateComponentFormats.Video)
                    return TemplateComponentFormat.Video;

                if (format == TemplateComponentFormats.Image)
                    return TemplateComponentFormat.Image;

                throw new NotImplementedException();
            }

            TemplateButtonType GetButtonTypeFromResponse(string type)
            {
                return type.ToLower() switch
                {
                    "url" => TemplateButtonType.Url,
                    "phone_number" => TemplateButtonType.Phone_Number,
                    "quick_reply" => TemplateButtonType.Quick_Reply,
                    _ => throw new NotImplementedException()
                };
            }
        }

        public async Task<bool> CheckPhoneNumberAsync(string phoneNumber)
        {
            var result = await SendTextMessageAsync(new SendMessageDto
            {
                Message = "TEST",
                To = phoneNumber
            });

            return result.Succeeded;
        }

        public async Task<ResultDto<CreateTemplateResponse>> CreateTemplateAsync(CreateTemplateDto dto) 
        {            
            var validator = new CreateTemplateCommandValidator();
            var validationResult = validator.Validate(dto);
            if (!validationResult.Succeeded)
            {
                return ResultDto<CreateTemplateResponse>.Failure(validationResult.Message);
            }

            var components = MapComponent(dto);

            var templateData = new
            {
                name = dto.Name, // اسم القالب (يجب أن يكون فريدًا)
                language = dto.Language == TemplateLanguage.Arabic ? "ar" : "en", // اللغة (مثال: "ar" للعربية)
                category = dto.Category.ToString(), // نوع القالب (AUTHENTICATION, MARKETING, UTILITY)
                components = components.Select<CreateTemplateComponent, object>(x => 
                {
                    if (x.Type == TemplateComponentType.Header) 
                    {
                        if (x.Format is null) 
                        {
                            return new 
                            {
                                format = TemplateComponentFormat.Text.ToString(),
                                type = x.Type.ToString(),
                                text = x.Text,
                            };
                        }
                        // With Variables
                        else if (x.Format == TemplateComponentFormat.Text)
                        {
                            return new
                            {
                                type = x.Type.ToString(),
                                format = x.Format.ToString(),
                                text = x.Text,
                                example =
                                x.Example is null || x.Example.HeaderText is null ? null :
                                new
                                {
                                    header_text = new List<string> { x.Example!.HeaderText! }
                                }
                            };
                        }
                        else
                        {
                            return new
                            {
                                type = x.Type.ToString(),
                                format = x.Format.ToString(),
                                example = x.Example is null || x.Example.HeaderHandle is null ? null : new
                                {
                                    header_handle = new List<string> { x.Example.HeaderHandle! }
                                }
                            };
                        }
                    }
                    
                    if (x.Type == TemplateComponentType.Body)
                    {
                        return new 
                        {
                            type = x.Type.ToString(),
                            text = x.Text,
                            example = 
                            x.Example is null || x.Example.BodyText is null ? null :
                            new
                            {
                                body_text = 
                                new List<List<string>>(){
                                    x.Example.BodyText
                                    .ToList() }
                            }
                        };
                    }
                    
                    if (x.Type == TemplateComponentType.Footer)
                    {
                        return new 
                        {
                            type = x.Type.ToString(),
                            text = x.Text
                        };
                    }
                
                    if (x.Type == TemplateComponentType.Buttons)
                    {
                        return new 
                        {
                            type = x.Type.ToString(),
                            buttons = x.Buttons!.Select<TemplateButtonDto, object>(o => 
                            {
                                if (o.Type == TemplateButtonType.Url)
                                {
                                    return new 
                                    {
                                        type = o.Type.ToString(),
                                        text = o.Text,
                                        url = o.Url
                                    };
                                }

                                if (o.Type == TemplateButtonType.Phone_Number)
                                {
                                    return new 
                                    {
                                        type = o.Type.ToString(),
                                        text = o.Text,
                                        phone_number = o.PhoneNumber
                                    };
                                }

                                if (o.Type == TemplateButtonType.Quick_Reply)
                                {
                                    return new 
                                    {
                                        type = o.Type.ToString(),
                                        text = o.Text,
                                        //quick_reply = new 
                                        //{
                                        //    // id = o.Id
                                        //}
                                    };
                                }

                                throw new NotImplementedException();
                            })
                        };
                    }
                    
                    throw new NotImplementedException();
                    // إنشاء كائن مجهول (anonymous object) ديناميكيًا بناءً على النوع
                    // if (x.Type == TemplateComponentType.Header && x.Format is not null)
                    // {
                    //     // إذا كان Header وله format (صورة/فيديو/مستند)
                    //     return new 
                    //     {
                    //         type = x.Type.ToString(),
                    //         format = x.Format.ToString(), // نضيف format فقط إذا لم يكن نصياً
                    //         example =
                    //     };
                    // }
     
                    // else
                    // {
                    // // المكونات الأخرى (BODY, FOOTER, BUTTONS)
                    //     return new 
                    //     {
                    //         type = x.Type.ToString(),
                    //         text = x.Text
                    //     };
                    // }
                })
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_configuration.Api + _configuration.AccountId}/message_templates");

            requestMessage.Headers.Add("Authorization", $"Bearer {_configuration.AccessToken}");

            var content = new StringContent(JsonSerializer.Serialize(templateData), Encoding.UTF8, "application/json");

            requestMessage.Content = content;

            using var _httpClient = new HttpClient();
            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Template created successfully !");

                var responseContent = await response.Content.ReadAsStringAsync();
                var templateResponse = JsonSerializer.Deserialize<CreateTemplateResponse>(responseContent);

                return ResultDto<CreateTemplateResponse>.Success(templateResponse!);
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError(errorContent);

            return ResultDto<CreateTemplateResponse>.Failure();

            List<CreateTemplateComponent> MapComponent(CreateTemplateDto component)
            {
                var components = new List<CreateTemplateComponent>();

                // Header
                if (component.Header is not null)
                {
                    components.Add(new CreateTemplateComponent
                    {
                        Type = component.Header.Type,
                        Format = component.Header.Format,
                        Text = component.Header.Text ,
                        Example = component.Header.Example
                    });
                }

                // Body
                components.Add(new CreateTemplateComponent
                {
                    Type = component.Body.Type,
                    Format = component.Body.Format,
                    Text = component.Body.Text,
                    Example = component.Body.Example
                });

                // Footer
                if (component.Footer is not null)
                {
                    components.Add(new CreateTemplateComponent
                    {
                        Type = component.Footer.Type,
                        Format = component.Footer.Format,
                        Text = component.Footer.Text,
                        Example = component.Footer.Example
                    });
                }

                // Buttons
                if (component.Buttons is not null)
                {
                    components.Add(new CreateTemplateComponent
                    {
                        Type = TemplateComponentType.Buttons,
                        Buttons = component.Buttons.Buttons
                    });
                }
                
                return components;
            }
        }
    
        public async Task<ResultDto<UploadMediaResponseDto>> UploadMediaAsync(UploadMediaDto dto)
        {
            if (ApiEnvironment.IsDevelopment(_settings.Environment))
                return ResultDto<UploadMediaResponseDto>.Success(new UploadMediaResponseDto { MediaId = "test-media-id" });

            _logger.LogInformation("Uploading media to WhatsApp at {0}", DateTimeCulture.Now);

            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_configuration.AccessToken}");

                using var formData = new MultipartFormDataContent();
                using var fileStream = dto.File.OpenReadStream();
                using var fileContent = new StreamContent(fileStream);
                formData.Add(fileContent, "file", dto.File.FileName);

                var response = await httpClient.PostAsync(
                    $"{_configuration.Api}{_configuration.PhoneNumberId}/media",
                    formData
                );

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Failed to upload media to WhatsApp: {0}", errorContent);
                    return ResultDto<UploadMediaResponseDto>.Failure($"Failed to upload media: {errorContent}");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var uploadResponse = JsonSerializer.Deserialize<UploadMediaResponseDto>(responseContent);
                
                if (uploadResponse == null)
                {
                    _logger.LogError("Failed to deserialize WhatsApp media upload response");
                    return ResultDto<UploadMediaResponseDto>.Failure("Failed to process media upload response");
                }

                _logger.LogInformation("Successfully uploaded media to WhatsApp with ID: {0}", uploadResponse.MediaId);
                return ResultDto<UploadMediaResponseDto>.Success(uploadResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading media to WhatsApp");
                return ResultDto<UploadMediaResponseDto>.Failure(ex.Message);
            }
        }
    
    }
}
