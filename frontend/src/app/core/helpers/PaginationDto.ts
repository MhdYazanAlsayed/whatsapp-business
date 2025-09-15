export default interface PaginationDto<T> {
  pages: number;
  data: T;
}
