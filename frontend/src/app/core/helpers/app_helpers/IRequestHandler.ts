export interface IRequestHandler<TRequest, TResponse> {
  handleAsync(request: TRequest): Promise<TResponse>;
}
