export class ResultDto<T> {
  succeeded: boolean;
  entity: T | null;

  constructor(succeeded: boolean, entity: T | null) {
    this.succeeded = succeeded;
    this.entity = entity;
  }

  static success<T>(entity: T | null = null): ResultDto<T> {
    return new ResultDto<T>(true, entity);
  }

  static failure<T>(entity: T | null = null): ResultDto<T> {
    return new ResultDto<T>(false, entity);
  }
}
export class SimpleResultDto {
  succeeded: boolean;

  constructor(succeeded: boolean) {
    this.succeeded = succeeded;
  }

  static success(): SimpleResultDto {
    return new SimpleResultDto(true);
  }

  static failure(): SimpleResultDto {
    return new SimpleResultDto(false);
  }
}
