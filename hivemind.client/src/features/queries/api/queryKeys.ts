export const queryKeys = {
    all: ['queries'] as const,

    lists: () => [...queryKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...queryKeys.lists(), filters] as const,

    details: () => [...queryKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...queryKeys.details(), id] as const,

    tests: () => [...queryKeys.all, 'tests'] as const,
};