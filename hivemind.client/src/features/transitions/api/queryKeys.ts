export const transitionKeys = {
    all: ['transitions'] as const,

    lists: () => [...transitionKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...transitionKeys.lists(), filters] as const,

    details: () => [...transitionKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...transitionKeys.details(), id] as const,
};