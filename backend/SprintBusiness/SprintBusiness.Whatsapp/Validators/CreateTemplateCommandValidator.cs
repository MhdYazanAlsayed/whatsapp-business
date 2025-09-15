using SprintBusiness.Domain.Templates.Enums;
using SprintBusiness.Domain.Templates.TemplateComponents.Enums;
using SprintBusiness.Shared.Dtos;
using SprintBusiness.Whatsapp.Dtos.TemplatesDtos.Create;

namespace SprintBusiness.Whatsapp.Validators
{
    public class CreateTemplateCommandValidator 
    {
        public ResultDto Validate(CreateTemplateDto command)
        {
            if (command.Name is null || command.Name.Length > 60 || command.Body is null)
                return ResultDto.Failure("Name is required and must be less than 60 characters");

            command.Name = command.Name
                .ToLower()
                .Replace(" ", "_")
                .Replace("-", "_")
                .Replace("@", "")
                .Replace("#", "")
                .Replace(".", "")
                .Replace("!", "")
                .Replace("?", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace(",", "")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("*", "")
                .Replace("+", "")
                .Replace("=", "")
                .Replace("|", "")
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("^", "")
                .Replace("~", "")
                .Replace("`", "")
                .Replace("$", "")
                .Replace("%", "")
                .Replace("&", "");
            

            // Remove format from header 
            if (command.Header is not null) 
            {
                if (command.Header.Text is not null)
                {
                    if (command.Header.Text.Contains("{{"))
                    {
                        if ((command.Header.Example is null || command.Header.Example.HeaderText is null))
                            return ResultDto.Failure("Header must have at least one example");

                        command.Header.Format = TemplateComponentFormat.Text;
                    }
                    else
                    {
                        command.Header.Format = null;
                    }

                }
                else
                {
                    command.Header.Text = null;

                    if (command.Header.Format is null) 
                        return ResultDto.Failure("Header must have a format");

                    if (command.Header.Example is null || command.Header.Example.HeaderHandle is null)
                        return ResultDto.Failure("Header must have at least one example");
                }
            }

            if (command.Body.Text is not null)
            {
                command.Body.Format = null;

                var numberOfVariables = command.Body.Text.Count(c => c == '{') / 2;
                if (numberOfVariables > 10)
                    return ResultDto.Failure("Body can't have more than 10 variables");

                if (command.Body.Example is null || command.Body.Example.BodyText is null || command.Body.Example.BodyText.Count < numberOfVariables)
                    return ResultDto.Failure("Body must have at least as many examples as variables");
            }
                
            if (command.Footer is not null && command.Footer.Text is not null)
            {
                command.Footer.Format = null;
            }
         
            if (command.Buttons is not null && command.Buttons.Buttons is not null)
            {
                if (command.Buttons.Buttons.Count > 3)
                    return ResultDto.Failure("Buttons can't have more than 3 buttons");

                foreach (var button in command.Buttons.Buttons)
                {
                    if (button.Text is null)
                        return ResultDto.Failure("Text is required");

                    if (button.Text.Length > 20)
                        return ResultDto.Failure("Text can't be more than 20 characters");


                    if (button.Type == TemplateButtonType.Url)
                    {
                        if (button.Url is null)
                            return ResultDto.Failure("Url is required");

                        button.PhoneNumber = null;

                    }
                  
                    if (button.Type == TemplateButtonType.Phone_Number)
                    {
                        if (button.PhoneNumber is null)
                            return ResultDto.Failure("Phone number is required");
                        
                        button.Url = null;
                    }
                   
                    if (button.Type == TemplateButtonType.Quick_Reply)
                    {
                        button.Url = null;
                        button.PhoneNumber = null;
                    }
                }
            }
            
            return ResultDto.Success();
        }
    }
}
