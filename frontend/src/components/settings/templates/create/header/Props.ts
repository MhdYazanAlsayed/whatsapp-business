import { FormData } from "../Props";

export default interface Props {
  direction: string;
  headerComponent: FormData;
  setHeaderComponent: (
    component: FormData | ((prev: FormData) => FormData)
  ) => void;
}
