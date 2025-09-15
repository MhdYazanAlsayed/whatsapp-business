import TadawiModal from "src/components/shared/modal/TadawiModal";
import Props from "./props/Props";
import AttachmentImage from "src/assets/images/attachment.png";
import { FormEvent, useEffect, useState } from "react";
import { SaveConversationNoteCommandPayload } from "src/app/features/conversation/commands/save-note/SaveConversationNoteCommandPayload";
import { useEvents } from "src/hooks/useEvents";
import ConversationNoteAttachment from "src/app/core/entities/conversations/conversation-notes/attachments/keys/ConversationNoteAttachment";
import MediatR from "src/app/core/helpers/mediatR/MediatR";
import UploadNoteAttachmentCommand from "src/app/features/conversation-notes/commands/upload-attachments/UploadNoteAttachmentCommand";
import SaveConversationNoteCommand from "src/app/features/conversation/commands/save-note/SaveConversationNoteCommand";
import UploadNoteAttachmentResult from "src/app/features/conversation-notes/commands/upload-attachments/UploadNoteAttachmentResult";
import DependenciesInjector from "src/app/core/util/DependenciesInjector";
import { toast } from "react-toastify";

let attachmentsAdded: File[] = [];
let attachmentsDeleted: ConversationNoteAttachment[] = [];

const _hostEnvironment = DependenciesInjector.services.hostEnviroment;
const NotesModal = ({
  open,
  setOpen,
  note,
  id,
  handleUpdateConversationNote,
}: Props) => {
  const [formData, setFormData] = useState<SaveConversationNoteCommandPayload>({
    id: id,
    attachmentsToAdd: [],
    attachmentsToDelete: [],
    content: note?.content ?? "",
  });
  const [attachments, setAttachments] = useState<ConversationNoteAttachment[]>(
    []
  );

  const { handleOnChange } = useEvents(setFormData);

  useEffect(() => {
    attachmentsAdded = [];
    attachmentsDeleted = [];

    if (note) {
      setAttachments([...note.attachments]);
      setFormData({
        id: id,
        attachmentsToAdd: [],
        attachmentsToDelete: [],
        content: note?.content ?? "",
      });
      return;
    }

    setAttachments([]);
  }, [open]);

  const handleOnSubmitAsync = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    // Upload attachments
    let uploadMediaResult: UploadNoteAttachmentResult | null = null;

    if (attachmentsAdded.length > 0) {
      uploadMediaResult = await MediatR.features.executeAsync(
        new UploadNoteAttachmentCommand({
          files: attachmentsAdded,
        })
      );
    }

    // Save note
    const result = await MediatR.features.executeAsync(
      new SaveConversationNoteCommand({
        ...formData,
        attachmentsToAdd: uploadMediaResult?.attachments ?? [],
        attachmentsToDelete: attachmentsDeleted.map((x) => ({
          attachmentId: x.id.value,
        })),
      })
    );

    // Update value
    handleUpdateConversationNote(result);

    toast.success("تم الحفظ بنجاح .");
  };

  const handleAddAttachments = (value: FileList | null) => {
    if (!value) return;

    let files: File[] = [];
    let startIndex = attachmentsAdded.length;
    for (let file of value) {
      files.push(file);
      attachmentsAdded.push(file);
    }

    setAttachments((prev) => {
      for (let x of files) {
        prev.push({
          conversationNoteId: null!,
          createdAt: null!,
          fileId: `NEW_${++startIndex}`,
          fileName: x.name,
          id: null!,
          conversationNote: null!,
        });
      }

      console.log(prev);

      return [...prev];
    });
  };

  const handleRemoveAttachment = (item: ConversationNoteAttachment) => {
    if (item.fileId.startsWith("NEW_")) {
      // Remove Only
      var index = parseInt(item.fileId.slice(4)) - 1;
      attachmentsAdded.splice(index, 1);

      setAttachments((prev) => {
        const indexAtt = prev.findIndex((x) => x.fileId == item.fileId);
        if (indexAtt === -1) throw new Error();

        prev.splice(indexAtt, 1);

        return [...prev];
      });
      return;
    }

    // Remove and push id on deleted array
    attachmentsDeleted.push(item);
    setAttachments((prev) => {
      const index = prev.findIndex((x) => x.id.value === item.id.value);
      if (index === -1) throw new Error();

      prev.splice(index, 1);

      return [...prev];
    });
  };

  return (
    <TadawiModal open={open} setOpen={setOpen} width="800px">
      <div className="mb-3">
        <p className="fw-semibold">قم باضافة ملاحظات</p>
      </div>

      <form onSubmit={handleOnSubmitAsync} className="row">
        <div className="col-md-8">
          <div className="tabs">
            <button className="active">الملاحظات</button>
          </div>

          <textarea
            className="form-control mb-3"
            rows={11}
            value={formData.content}
            onChange={(x) => handleOnChange("content", x.currentTarget.value)}
          ></textarea>

          <div className="notes-attachment-input mb-3">
            <div className="d-flex align-items-center gap-1">
              <svg
                width="26"
                height="25"
                viewBox="0 0 21 20"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M7.4657 6.66748V5.00081C7.4657 3.15986 8.95808 1.66748 10.799 1.66748C12.6399 1.66748 14.1324 3.15986 14.1324 5.00081V15.0008C14.1324 16.8417 12.6399 18.3342 10.799 18.3342C8.95808 18.3342 7.4657 16.8417 7.4657 15.0008V11.2508C7.4657 10.1002 8.39844 9.16749 9.54903 9.16749C10.6996 9.16749 11.6324 10.1002 11.6324 11.2508V13.3342"
                  stroke="#646D74"
                  strokeWidth="1.2"
                  strokeLinecap="round"
                  strokeLinejoin="round"
                ></path>
              </svg>
              <p className="text-muted">اضف مرفقات</p>

              <input
                type="file"
                multiple
                onChange={(x) => handleAddAttachments(x.currentTarget.files)}
              />
            </div>
          </div>

          <div className="d-flex align-items-center gap-2">
            <button
              className="custom-btn py-1 secondary rounded"
              style={{ width: "120px" }}
              onClick={(x) => {
                x.preventDefault();

                setOpen(false);
              }}
            >
              الغاء
            </button>
            <button
              style={{ width: "120px" }}
              className="custom-btn py-1 primary rounded"
            >
              حفظ
            </button>
          </div>
        </div>
        <div className="col-md-4">
          <p className="mb-3">المرفقات</p>

          <div className="d-flex flex-column gap-2">
            {attachments.map((x, index) => (
              <div
                className="attachment d-flex align-items-center gap-2"
                key={index}
              >
                <a
                  href={
                    x.id == null
                      ? "!"
                      : `${_hostEnvironment.apiUrl}ConversationNoteAttachments/${x.fileId}`
                  }
                  download={x.id != null}
                  className="d-flex align-items-center gap-2"
                >
                  <img src={AttachmentImage} style={{ width: "20px" }} />
                  <p>
                    {x.fileName.length > 12
                      ? x.fileName.slice(0, 12) + "..."
                      : x.fileName}
                  </p>
                </a>

                <i
                  className="fa-solid fa-trash text-danger cursor-pointer me-auto"
                  onClick={(a) => {
                    a.preventDefault();

                    handleRemoveAttachment(x);
                  }}
                ></i>
              </div>
            ))}
          </div>
        </div>
      </form>
    </TadawiModal>
  );
};

export default NotesModal;
