export function useQuery(search: any) {
  if (!search || search.length == 0) return null;
  const result: any = {};

  const query: string = search;
  const parameters: string[] = query.slice(1).split("&");

  parameters.forEach((parameter) => {
    const equalIndex: number = parameter.indexOf("=");
    const key: string = parameter.slice(0, equalIndex);
    const value: string = parameter.slice(equalIndex + 1);

    result[key] = value;
  });

  return result as any;
}
