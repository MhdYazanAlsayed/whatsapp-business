export interface IRequestHandler<TRequest, TResponse> {
  HandleAsync(request: TRequest): Promise<TResponse>;
}
