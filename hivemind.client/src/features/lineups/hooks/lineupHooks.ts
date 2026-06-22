import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createLineup, deleteLineup, updateLineup } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { queryKeys } from '../api/queryKeys';

export const useLineupCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createLineup,
        onSuccess: () => {
            showNotification("Lineup created", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.all });
        },
        onError: () => {
            showNotification("Failed to create lineup", "error");
        },
    });
};

export const useLineupDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteLineup(id),
        onSuccess: () => {
            showNotification("Lineup deleted", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.all });
        },
        onError: () => {
            showNotification("Failed to delete lineup", "error");
        },
    });
};

export const useLineupUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateLineup,
        onSuccess: () => {
            showNotification("Lineup updated", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.all });
        },
        onError: () => {
            showNotification("Failed to update lineup", "error");
        },
    });
};