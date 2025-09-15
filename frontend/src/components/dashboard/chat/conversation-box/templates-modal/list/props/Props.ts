import Template from "src/app/core/entities/templates/Template";

export default interface Props {
  templates: Template[];
  selected: Template | null;
  handleGetDetailsAsync(data: Template): Promise<void>;
}
