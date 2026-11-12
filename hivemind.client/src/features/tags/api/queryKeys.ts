export const tagKeys = {
    all: ['Tags'] as const,

    lists: () => [...tagKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...tagKeys.lists(), filters] as const,

    details: () => [...tagKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...tagKeys.details(), id] as const,
};