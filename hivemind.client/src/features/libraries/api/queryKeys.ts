export const libraryKeys = {
    all: ['libraries'] as const,

    lists: () => [...libraryKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...libraryKeys.lists(), filters] as const,

    details: () => [...libraryKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...libraryKeys.details(), id] as const,
};