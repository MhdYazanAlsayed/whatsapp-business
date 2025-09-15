import { FormEvent, useEffect, useState } from "react";
import { toast } from "react-toastify";
import Template from "src/app/core/entities/templates/Template";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
import SyncTemplatesCommand from "src/app/features/templates/commands/sync-temlates/SyncTemplatesCommand";
import GetTemplateDetailsQuery from "src/app/features/templates/queries/details/GetTemplateDetailsQuery";
import GetAllTemplateQuery from "src/app/features/templates/queries/get-all/GetAllTemplatesQuery";
import TadawiModal from "src/components/shared/modal/TadawiModal";
import TemplateForm from "./TemplateForm";
import TemplateFormData, {
  TemplateBodyFormData,
} from "./types/TemplateFormData";
import TemplatesList from "./list/TemplatesList";
import HeaderModal from "./header/HeaderModal";
import SendTemplateCommand from "src/app/features/conversation/commands/send-template/SendTemplateCommand";
import { TemplateComponentType } from "src/app/core/entities/templates/template-components/enums/TemplateComponentType";
import { SendTemplateComponentCommandPayload } from "src/app/features/conversation/commands/send-template/payloads/SendTemplateComponentCommandPayload";
import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import { TemplateParameterType } from "src/app/features/conversation/commands/send-template/enums/TemplateParameterType";
import { SendTemplateParameterCommandPayload } from "src/app/features/conversation/commands/send-template/payloads/SendTemplateParameterCommandPayload";
import UploadTemplateMediaCommand from "src/app/features/templates/commands/upload-media/UploadTemplateMediaCommand";
import { useParams } from "react-router-dom";
import Props from "./props/Props";

let firstRender = true;

const TemplateModal = ({ open, setOpen, handleAddMoreMessages }: Props) => {
  const [templates, setTemplates] = useState<Template[]>([]);
  const [selected, setSelected] = useState<Template | null>(null);
  const [formData, setFormData] = useState<TemplateFormData>({
    header: {
      file: null,
    },
    body: [],
  });

  const { id } = useParams();

  useEffect(() => {
    if (firstRender && open) {
      handleGetTemplatesAsync();
      firstRender = false;
    }
  }, [open]);

  // Reset
  useEffect(() => {
    setFormData({
      header: {
        file: null,
      },
      body: [],
    });
  }, [selected]);

  const handleGetTemplatesAsync = async () => {
    const result = await MediatR.features.executeAsync(
      new GetAllTemplateQuery()
    );

    setTemplates(result);
  };

  const handleSyncTemplatesAsync = async () => {
    const result = await MediatR.features.executeAsync(
      new SyncTemplatesCommand()
    );

    if (!result.succeeded) {
      toast.error(
        "حدث خطأ اثناء عمل مزامنة للقوالب , تواصل مع فريق التطوير لحل هذه المشكلة ."
      );
      return;
    }

    handleGetTemplatesAsync();
  };

  const handleGetDetailsAsync = async (template: Template) => {
    const result = await MediatR.features.executeAsync(
      new GetTemplateDetailsQuery({ templateId: template.id })
    );

    setSelected(result);
  };

  const handleOnSubmitAsync = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!selected) return;

    const components: SendTemplateComponentCommandPayload[] = [];

    // Setup headers parameters
    await handleSetupHeaderParameterRequestAsync(components);

    // Setup bodys parameters
    handleSetupBodyParameterRequest(components);

    const result = await MediatR.features.executeAsync(
      new SendTemplateCommand({
        conversationId: { value: parseInt(id!) },
        templateId: selected!.id,
        components: components,
      })
    );

    if (!result.succeeded || !result.entity) {
      toast.error("حدث خطأ ما .");
      return;
    }

    handleAddMoreMessages([{ ...result.entity }]);

    setOpen(false);
  };

  // Header of request
  const handleSetupHeaderParameterRequestAsync = async (
    components: SendTemplateComponentCommandPayload[]
  ) => {
    const header = selected!.components.find(
      (x) => x.type === TemplateComponentType.Header
    );
    if (!header) return;

    if (!formData.header.file) {
      toast.error("جميع حقول القالب مطلوبة .");
      throw new Error();
    }

    var fileName = await handleUploadMediaAsync(formData.header.file);

    components.push({
      type: TemplateComponentType.Header,
      parameters: [
        {
          ...handleGetHeaderParameterFromFormat(header.format, fileName),
        },
      ],
    });
  };

  const handleUploadMediaAsync = async (file: File) => {
    const result = await MediatR.features.executeAsync(
      new UploadTemplateMediaCommand({
        file: file,
      })
    );

    return result.fileName;
  };

  const handleGetHeaderParameterFromFormat = (
    format: TemplateComponentFormat,
    payload: string
  ): SendTemplateParameterCommandPayload => {
    if (format === TemplateComponentFormat.Text)
      return { type: TemplateParameterType.Text, text: payload };

    if (format === TemplateComponentFormat.Document)
      return {
        type: TemplateParameterType.Document,
        document: { fileName: payload },
      };

    if (format === TemplateComponentFormat.Image)
      return {
        type: TemplateParameterType.Image,
        image: { fileName: payload },
      };

    if (format === TemplateComponentFormat.Video)
      return {
        type: TemplateParameterType.Video,
        video: { fileName: payload },
      };

    throw new Error("");
  };

  // Body of request
  const handleSetupBodyParameterRequest = (
    components: SendTemplateComponentCommandPayload[]
  ) => {
    const parameters: SendTemplateParameterCommandPayload[] = [];
    for (let parameter of formData.body.sort((a, b) => a.number - b.number)) {
      parameters.push({
        type: TemplateParameterType.Text,
        text: parameter.value,
      });
    }

    components.push({
      type: TemplateComponentType.Body,
      parameters: parameters,
    });
  };

  //#region Components Events

  // ? Function to add header parameters
  const handleAddHeaderParameterValue = (file: File | null) => {
    setFormData((prev) => {
      prev.header.file = file;

      return { ...prev };
    });
  };

  // ? Function to add body parameters
  const handleAddBodyParameterValue = ({
    value,
    number,
  }: TemplateBodyFormData) => {
    setFormData((prev) => {
      const index = prev.body.findIndex((x) => x.number == number);
      if (index === -1) {
        prev.body.push({ value, number });
        return { ...prev };
      }

      prev.body[index].value = value;

      return { ...prev };
    });
  };

  //#endregion

  return (
    <TadawiModal
      open={open}
      setOpen={setOpen}
      width="70%"
      className="ready-responses-modal"
    >
      <HeaderModal handleSyncTemplatesAsync={handleSyncTemplatesAsync} />

      <form onSubmit={handleOnSubmitAsync} className="row">
        <TemplatesList
          templates={templates}
          selected={selected}
          handleGetDetailsAsync={handleGetDetailsAsync}
        />

        <div className="col-6">
          <div className="selected-response ">
            <div className="response-content gap-2">
              <TemplateForm
                template={selected!}
                formData={formData}
                handleAddBodyParameterValue={handleAddBodyParameterValue}
                handleAddHeaderParameterValue={handleAddHeaderParameterValue}
              />

              <div className="buttons">
                <button
                  className="custom-btn secondary"
                  onClick={(x) => {
                    x.preventDefault();

                    setOpen(false);
                  }}
                >
                  اغلاق
                </button>
                <button
                  className="custom-btn primary"
                  disabled={selected === null}
                  type="submit"
                >
                  ارسال
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
    </TadawiModal>
  );
};

export default TemplateModal;
