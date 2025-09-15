import ComponentProps from "../../props/ComponentProps";

export default interface Props extends ComponentProps {
  fileName: string;
  handleAddHeaderParameterValue(file: File | null): void;
}
