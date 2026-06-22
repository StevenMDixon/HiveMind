export const queryKeys = {
    all: ['lineups'] as const,

    lists: () => [...queryKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...queryKeys.lists(), filters] as const,

    details: () => [...queryKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...queryKeys.details(), id] as const,

    lineItems: () => [...queryKeys.all, 'lineItem'] as const,

    lineItem: (id: number) => [...queryKeys.all, 'lineItem', id] as const,
};