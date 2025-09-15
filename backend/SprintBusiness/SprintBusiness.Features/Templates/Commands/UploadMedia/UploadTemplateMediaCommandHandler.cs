using MediatR;
using SprintBuisness.Contracts.Services;
using SprintBusiness.Shared.Dtos;

namespace SprintBusiness.Features.Templates.Commands.UploadMedia
{
    public class UploadTemplateMediaCommandHandler : IRequestHandler<UploadTemplateMediaCommand, ResultDto<UploadTemplateMediaResult>>
    {
        private readonly IFileManager _fileManager;

        public UploadTemplateMediaCommandHandler(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public async Task<ResultDto<UploadTemplateMediaResult>> Handle(UploadTemplateMediaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fileName = await _fileManager.SaveAsync("Templates", request.File);

                return ResultDto<UploadTemplateMediaResult>.Success(new UploadTemplateMediaResult { FileName = fileName });
            }
            catch (Exception ex)
            {
                return ResultDto<UploadTemplateMediaResult>.Failure(ex.Message);
            }
        }
    }
}
