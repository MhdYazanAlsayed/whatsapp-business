import { useEffect, useState } from "react";
import { TemplateCategory } from "src/app/core/entities/templates/enums/TemplateCategory";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";
import { TemplateLanguage } from "src/app/core/entities/templates/enums/TemplateLanguage";
import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { useEvents } from "src/hooks/useEvents";
import TemplateView from "../index/TemplateView";
import { TemplateStatus } from "src/app/core/entities/templates/enums/TemplateStatus";
import Template from "src/app/core/entities/templates/Template";
import { useNavigate } from "react-router-dom";
import Header from "./header/Header";
import { FormData } from "./Props";
import Body from "./body/Body";
import Footer from "./footer/Footer";
import Buttons from "./buttons/Buttons";
import CreateTemplatePayload, {
  TemplateButtonPayload,
} from "src/app/features/templates/commands/create/CreateTemplatePayload";
import SubmitButton from "./SubmitButton";
import CreateTemplateCommand from "src/app/features/templates/commands/create/CreateTemplateCommand";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
import { toast } from "react-toastify";

const Create = () => {
  const [formData, setFormData] = useState<{
    name: string;
    language: TemplateLanguage;
    category: TemplateCategory;
  }>({
    name: "",
    language: TemplateLanguage.Arabic,
    category: TemplateCategory.Utility,
  });
  const [headerComponent, setHeaderComponent] = useState<FormData>({
    type: TemplateComponentType.Header,
    text: "",
    format: -1,
    example: {
      headerText: [],
      bodyText: [],
      footerText: [],
    },
  });
  const [bodyComponent, setBodyComponent] = useState<FormData>({
    type: TemplateComponentType.Body,
    text: "",
    format: -1,
    example: {
      headerText: [],
      bodyText: [],
      footerText: [],
    },
  });
  const [footerComponent, setFooterComponent] = useState("");

  const [buttons, setButtons] = useState<TemplateButtonPayload[]>([]);
  const [templateToView, setTemplateToView] = useState<Template | null>(null);

  const { handleOnChange } = useEvents(setFormData);
  const navigate = useNavigate();

  useEffect(() => {
    const template: Template = {
      id: { value: -1 },
      name: formData.name,
      status: TemplateStatus.PENDING,
      category: formData.category,
      subCategory: "",
      language: formData.language,
      components: [
        {
          id: { value: -1 },
          templateId: { value: -1 },
          type: TemplateComponentType.Body,
          text: bodyComponent.text,
          format: TemplateComponentFormat.Text,
          variables: [],
          createdAt: new Date().toISOString(),
        },
      ],
      buttons: [],
      createdAt: new Date().toISOString(),
    };

    for (const button of buttons) {
      template.buttons.push({
        id: { value: -1 },
        templateId: { value: -1 },
        text: button.text,
        url: button.url,
        createdAt: new Date().toISOString(),
        type: button.type,
      });
    }

    if (headerComponent.format !== -1) {
      template.components.push({
        id: { value: -1 },
        templateId: { value: -1 },
        type: headerComponent.type,
        text: headerComponent.text,
        format: headerComponent.format as TemplateComponentFormat,
        variables: [],
        createdAt: new Date().toISOString(),
      });
    }

    if (footerComponent.trim() !== "") {
      template.components.push({
        id: { value: -1 },
        templateId: { value: -1 },
        type: TemplateComponentType.Footer,
        text: footerComponent,
        format: TemplateComponentFormat.Text,
        variables: [],
        createdAt: new Date().toISOString(),
      });
    }

    setTemplateToView(template);
  }, [
    headerComponent,
    bodyComponent,
    footerComponent,
    formData.language,
    buttons,
  ]);

  const handleSubmitAsync = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    let requestData: CreateTemplatePayload = {
      name: formData.name,
      language: formData.language,
      category: formData.category,
      body: {
        type: TemplateComponentType.Body,
        text: bodyComponent.text,
        format: TemplateComponentFormat.Text,
        example: {
          bodyText: bodyComponent.example.bodyText,
        },
      },
    };

    if (headerComponent.format !== -1) {
      requestData.header = {
        type: TemplateComponentType.Header,
        text: headerComponent.text,
        format: headerComponent.format,
        example: {
          headerText: headerComponent.example.headerText[0],
          headerHandle: "", //! We have add media in here
        },
      };
    }

    if (footerComponent.trim() !== "") {
      requestData.footer = {
        type: TemplateComponentType.Footer,
        text: footerComponent,
        format: TemplateComponentFormat.Text,
      };
    }

    if (buttons.length > 0) {
      requestData.buttons = {
        type: TemplateComponentType.Buttons,
        buttons: buttons,
      };
    }

    const response = await MediatR.features.executeAsync(
      new CreateTemplateCommand(requestData)
    );

    if (response.succeeded) {
      navigate("/settings/templates");
      return;
    }
    toast.error("حدث خطأ ما أثناء إنشاء القالب");
  };

  const direction =
    formData.language === TemplateLanguage.Arabic ? "rtl" : "ltr";

  return (
    <div className="container">
      <div className="row">
        <div className="col-md-8">
          <div className="thumpnail-custom p-2 screen-height d-flex flex-column">
            <h5 className="d-flex justify-content-between align-items-center mb-3">
              <div className="text-blue fw-semibold p-2">انشاء قالب 📔</div>
            </h5>

            <form className="overflow-auto" onSubmit={handleSubmitAsync}>
              <div className="container-fluid">
                <div className="row">
                  <div className="col-md-12 mb-3">
                    <div className="form-group">
                      <label
                        htmlFor="name"
                        className="mb-1 text-muted fw-semibold"
                      >
                        اسم القالب
                        <span className="text-danger">*</span>
                      </label>
                      <input
                        type="text"
                        className="form-control"
                        id="name"
                        value={formData.name}
                        onChange={(e) => handleOnChange("name", e.target.value)}
                      />
                    </div>
                  </div>

                  <div className="col-md-6 mb-3">
                    <div className="form-group">
                      <label
                        htmlFor="language"
                        className="mb-1 text-muted fw-semibold"
                      >
                        اللغة
                        <span className="text-danger">*</span>
                      </label>

                      <select
                        className="form-control"
                        id="language"
                        value={formData.language}
                        onChange={(e) =>
                          handleOnChange("language", parseInt(e.target.value))
                        }
                      >
                        <option value={TemplateLanguage.Arabic}>عربي</option>
                        <option value={TemplateLanguage.English}>
                          انجليزي
                        </option>
                      </select>
                    </div>
                  </div>

                  <div className="col-md-6 mb-3">
                    <div className="form-group">
                      <label
                        htmlFor="category"
                        className="mb-1 text-muted fw-semibold"
                      >
                        التصنيف
                        <span className="text-danger">*</span>
                      </label>

                      <select
                        className="form-control"
                        id="category"
                        value={formData.category}
                        onChange={(e) =>
                          handleOnChange("category", parseInt(e.target.value))
                        }
                      >
                        <option value={TemplateCategory.Utility}>
                          استفسار
                        </option>
                        <option value={TemplateCategory.Marketing}>
                          ترويج
                        </option>
                      </select>
                    </div>
                  </div>

                  <div className="col-md-12">
                    <hr />
                  </div>

                  {/* Header Component */}
                  <Header
                    direction={direction}
                    headerComponent={headerComponent}
                    setHeaderComponent={setHeaderComponent}
                  />

                  <div className="col-md-12">
                    <hr />
                  </div>

                  {/* Body Component */}
                  <Body
                    direction={direction}
                    bodyComponent={bodyComponent}
                    setBodyComponent={setBodyComponent}
                  />

                  <div className="col-md-12">
                    <hr />
                  </div>

                  {/* Footer Component */}
                  <Footer
                    footerComponent={footerComponent}
                    setFooterComponent={setFooterComponent}
                  />

                  <div className="col-md-12">
                    <hr />
                  </div>

                  {/* Buttons */}
                  <Buttons buttons={buttons} setButtons={setButtons} />

                  <div className="col-md-12 mb-3">
                    <SubmitButton
                      headerComponent={headerComponent}
                      bodyComponent={bodyComponent}
                      formData={formData}
                      footerComponent={footerComponent}
                      buttons={buttons}
                    />
                  </div>
                </div>
              </div>
            </form>
          </div>
        </div>
        <div className="col-md-4">
          <div className="thumpnail-custom py-2 px-4 screen-height d-flex flex-column overflow-auto">
            <div className="d-flex justify-content-center align-items-center">
              <div className="px-5 py-2 bg-light rounded text-muted fw-bold">
                معاينة الحملة
              </div>
            </div>

            <div className="pt-5">
              {templateToView ? (
                <TemplateView template={templateToView} />
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
      </div>
    </div>
  );
};

export default Create;
