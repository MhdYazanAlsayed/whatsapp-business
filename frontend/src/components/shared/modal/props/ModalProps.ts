import TadawiModalProps from "src/app/core/helpers/TadawiModalProps";

export default interface ModalProps extends TadawiModalProps {
  children: any;
  width?: string;
  height?: string;
  className?: string;
}
