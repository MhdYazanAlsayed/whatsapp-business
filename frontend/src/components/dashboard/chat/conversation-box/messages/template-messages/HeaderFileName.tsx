import DependenciesInjector from "src/app/core/util/DependenciesInjector";

const _hostEnvironment = DependenciesInjector.services.hostEnviroment;

export const HeaderFileName = ({ fileName }: { fileName: string }) => {
  if (
    fileName.endsWith("jpg") ||
    fileName.endsWith("png") ||
    fileName.endsWith("jpge")
  ) {
    return (
      <a
        href={`${_hostEnvironment.apiUrl}Templates/${fileName}`}
        target="_blank"
        className="template-image p-0"
        style={{ height: "auto" }}
      >
        <img
          src={_hostEnvironment.apiUrl + "Templates/" + fileName}
          className="img-fluid rounded w-100"
          style={{ height: "100px", objectFit: "cover" }}
        />
      </a>
    );
  }

  if (fileName.endsWith("docx")) {
    return (
      <div className="template-video">
        <a href={_hostEnvironment.apiUrl + "Templates/" + fileName} download>
          ملف Word
        </a>
      </div>
    );
  }

  if (fileName.endsWith("xlsx")) {
    <a href={_hostEnvironment.apiUrl + "Templates/" + fileName} download>
      ملف اكسل
    </a>;
  }

  if (
    fileName.endsWith("mp4") ||
    fileName.endsWith("avi") ||
    fileName.endsWith("mkv") ||
    fileName.endsWith("mov") ||
    fileName.endsWith("wmv") ||
    fileName.endsWith("flv") ||
    fileName.endsWith("webm") ||
    fileName.endsWith("mpg") ||
    fileName.endsWith("mpeg") ||
    fileName.endsWith("ogv") ||
    fileName.endsWith("vob")
  ) {
    return <>ملف فيديو</>;
  }

  throw new Error("Not implemented");
};
export default HeaderFileName;
