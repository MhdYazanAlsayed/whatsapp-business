import SelectOption from "src/app/core/helpers/SelectOption";

export default interface ReactSelectProps {
  placeholder?: string;
  options?: SelectOption[];
  isMulti?: boolean;
  onChange?: (data: SelectOption) => void;
  onMultiChange?: (data: SelectOption[]) => void;
}
