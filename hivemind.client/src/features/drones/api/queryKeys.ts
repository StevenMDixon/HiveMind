export const droneKeys = {
    all: ['drones'] as const,

    lists: () => [...droneKeys.all, 'list'] as const,

    list: (filters?: { active?: boolean }) =>
        [...droneKeys.lists(), filters] as const,

    details: () => [...droneKeys.all, 'detail'] as const,

    detail: (id: number) =>
        [...droneKeys.details(), id] as const,
};