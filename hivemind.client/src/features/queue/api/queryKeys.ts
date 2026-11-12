export const queueKeys = {
    all: ['Queue'] as const,

    lists: () => [...queueKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...queueKeys.lists(), filters] as const,

    details: () => [...queueKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...queueKeys.details(), id] as const,
};