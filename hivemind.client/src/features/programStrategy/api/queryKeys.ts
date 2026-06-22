export const strategyKeys = {
    all: ['strategies'] as const,

    lists: () => [...strategyKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...strategyKeys.lists(), filters] as const,

    details: () => [...strategyKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...strategyKeys.details(), id] as const,
};