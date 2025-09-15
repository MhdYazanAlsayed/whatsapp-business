import { useState, useEffect } from "react";
import { TemplateLanguage } from "src/app/core/entities/templates/enums/TemplateLanguage";
import { TemplateStatus } from "src/app/core/entities/templates/enums/TemplateStatus";
import Template from "src/app/core/entities/templates/Template";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
import SyncTemplatesCommand from "src/app/features/templates/commands/sync-temlates/SyncTemplatesCommand";
import GetTemplateDetailsQuery from "src/app/features/templates/queries/details/GetTemplateDetailsQuery";
import GetAllTemplateQuery from "src/app/features/templates/queries/get-all/GetAllTemplatesQuery";
import TemplateView from "./TemplateView";
import { Link } from "react-router-dom";
const Index = () => {
  const [templates, setTemplates] = useState<Template[]>([]);
  const [selectedTemplate, setSelectedTemplate] = useState<Template | null>(
    null
  );

  useEffect(() => {
    handleGetTemplates();
  }, []);

  const handleGetTemplates = async () => {
    const result = await MediatR.features.executeAsync(
      new GetAllTemplateQuery()
    );

    setTemplates(result);
  };

  const handleSyncTemplatesAsync = async () => {
    const result = await MediatR.features.executeAsync(
      new SyncTemplatesCommand()
    );

    if (result.succeeded) {
      handleGetTemplates();
    }
  };

  const handleGetTemplateDetailsAsync = async (id: number) => {
    const template = await MediatR.features.executeAsync(
      new GetTemplateDetailsQuery({ templateId: { value: id } })
    );

    if (template) {
      setSelectedTemplate(template);
    }
  };

  return (
    <div className="row">
      <div className="col-md-8">
        <div className="thumpnail-custom p-2 screen-height d-flex flex-column">
          <h5 className="d-flex justify-content-between align-items-center mb-3">
            <div className="text-blue fw-semibold p-2">القوالب</div>
            <div className="d-flex align-items-center gap-2">
              <Link
                to="/settings/templates/create"
                className="custom-btn primary rounded"
              >
                <div className="d-flex align-items-center gap-2">
                  <i className="fa-solid fa-plus"></i>
                </div>
              </Link>

              <button
                className="custom-btn primary rounded"
                onClick={handleSyncTemplatesAsync}
              >
                <div className="d-flex align-items-center gap-2">
                  <i className="fa-solid fa-download"></i>
                </div>
              </button>
            </div>
          </h5>

          <div className="table-scroll">
            <table
              className="table table-striped table-hover"
              style={{ backgroundColor: "transparent" }}
            >
              <thead>
                <tr>
                  <th className="text-muted">العنوان</th>
                  <th className="text-muted">الحالة</th>
                  <th className="text-muted">اللغة</th>
                  <th className="text-muted">تاريخ الانشاء</th>
                </tr>
              </thead>
              <tbody>
                {templates.map((template) => (
                  <tr
                    key={template.id.value.toString()}
                    className="cursor-pointer"
                    onClick={() =>
                      handleGetTemplateDetailsAsync(template.id.value)
                    }
                  >
                    <td className="text-muted">{template.name}</td>
                    <td className="text-muted">
                      {template.status === TemplateStatus.APPROVED
                        ? "معتمد"
                        : template.status === TemplateStatus.PENDING
                        ? "قيد المراجعة"
                        : "مرفوض"}
                    </td>
                    <td className="text-muted">
                      {template.language === TemplateLanguage.English
                        ? "انجليزي"
                        : "عربي"}
                    </td>
                    <td className="text-muted">
                      {new Date(template.createdAt).toLocaleDateString()}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </div>
      <div className="col-md-4">
        <div className="thumpnail-custom py-2 px-4 screen-height d-flex flex-column overflow-auto">
          <div className="d-flex justify-content-center align-items-center">
            <div className="px-5 py-2 bg-light rounded text-muted fw-bold">
              معاينة الحملة
            </div>
          </div>

          {selectedTemplate ? (
            <div className="pt-5">
              <TemplateView template={selectedTemplate} />
            </div>
          ) : (
            <div className="d-flex justify-content-center align-items-center flex-grow-1">
              <small className="d-block text-center text-muted">
                قم بتحديد قالب للعرض
              </small>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default Index;
