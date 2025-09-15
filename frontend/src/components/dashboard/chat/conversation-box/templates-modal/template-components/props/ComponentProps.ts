import TemplateComponent from "src/app/core/entities/templates/template-components/TemplateComponent";

export default interface ComponentProps {
  isRTL: boolean;
  data: TemplateComponent | undefined;
}
