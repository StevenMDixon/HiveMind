export const showKeys = {
    all: ['shows'] as const,

    lists: () => [...showKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...showKeys.lists(), filters] as const,

    details: () => [...showKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...showKeys.details(), id] as const,
};