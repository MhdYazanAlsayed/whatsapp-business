import { TemplateComponentFormat } from "src/app/core/entities/templates/template-components/enums/TemplateComponentFormat";
import Props from "./Props";
import HeaderFileEditor from "./HeaderFileEditor";
import HeaderTextEditor from "./HeaderTextEditor";

const Header = ({ direction, headerComponent, setHeaderComponent }: Props) => {

  return (
    <div className="mb-3">
      <h5 className="py-3 mb-3 text-blue fw-semibold">رأس القالب</h5>

      {/* Format */}
      <div className="col-md-12 mb-3">
        <div className="form-group">
          <label htmlFor="format" className="mb-1 text-muted fw-semibold">
            تنسيق الرأس
            <span className="text-danger">*</span>
          </label>
          <select
            className="form-control"
            value={headerComponent.format}
            onChange={(e) =>
              setHeaderComponent({
                ...headerComponent,
                format: parseInt(e.target.value),
              })
            }
          >
            <option value={-1}>لا يوجد</option>
            <option value={TemplateComponentFormat.Text}>نص</option>
            <option value={TemplateComponentFormat.Document}>ملف</option>
            <option value={TemplateComponentFormat.Image}>صورة</option>
            <option value={TemplateComponentFormat.Video}>فيديو</option>
          </select>
        </div>
      </div>

      {headerComponent.format == -1 ? null : headerComponent.format ===
        TemplateComponentFormat.Text ? (
        <HeaderTextEditor
          direction={direction}
          headerComponent={headerComponent}
          setHeaderComponent={setHeaderComponent}
        />
      ) : (
        <HeaderFileEditor
          direction={direction}
          headerComponent={headerComponent}
          setHeaderComponent={setHeaderComponent}
        />
      )}
    </div>
  );
};

// Text Editor

export default Header;
