import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createLineupItem, updateLineupItem  } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { queryKeys } from '../api/queryKeys';

export const useLineupItemCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createLineupItem,
        onSuccess: () => {
            showNotification("Lineup Item created", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.all });
        },
        onError: () => {
            showNotification("Failed to create lineup", "error");
        },
    });
};

export const useLineupItemUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateLineupItem,
        onSuccess: (_, lineupItem) => {
            showNotification("Lineup Item updated", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.lineItem(lineupItem.lineupItemId) });

        },
        onError: () => {
            showNotification("Failed to update lineup item", "error");
        },
    });
}
