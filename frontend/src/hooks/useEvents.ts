export const useEvents = (setFormData: any) => {
  function handleOnChange<T>(key: string, value: T) {
    setFormData((prev: any) => ({
      ...prev,
      [key]: value,
    }));
  }

  return { handleOnChange };
};
