export const stationKeys = {
    all: ['stations'] as const,

    lists: () => [...stationKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...stationKeys.lists(), filters] as const,

    details: () => [...stationKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...stationKeys.details(), id] as const,
};