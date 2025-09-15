export interface ResultDto<T> {
  succeeded: boolean;
  message?: string;
  entity?: T;
}
