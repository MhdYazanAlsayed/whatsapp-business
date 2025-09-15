import Select from "react-select";
import ReactSelectProps from "./props/ReactSelectProps";

const ReactSelect = ({
  options,
  onChange,
  onMultiChange,
  isMulti,
}: ReactSelectProps) => {
  let onChangeFunc: any = null;

  if (isMulti) {
    onChangeFunc = onMultiChange;
  } else {
    onChangeFunc = onChange;
  }

  return (
    <Select
      placeholder={"--- اختر عنصر ---"}
      options={options}
      noOptionsMessage={() => "لا توجد بيانات"}
      onChange={onChangeFunc}
    />
  );
};

export default ReactSelect;
