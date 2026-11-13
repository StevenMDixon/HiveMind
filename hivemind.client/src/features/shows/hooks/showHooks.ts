import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createShow, updateShow } from '../api/requests';
import { useGlobalNotification } from '@/Dashboard/useGlobalNotification';
import { showKeys } from '../api/queryKeys';

export const useLibraryCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createShow,
        onSuccess: () => {
            showNotification("Show created", "success");
            queryClient.invalidateQueries({ queryKey: showKeys.all });
        },
        onError: () => {
            showNotification("Failed to create show", "error");
        },
    });
};


export const useShowUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateShow,
        onSuccess: () => {
            showNotification("Show updated", "success");
            queryClient.invalidateQueries({ queryKey: showKeys.all });
        },
        onError: () => {
            showNotification("Failed to update show", "error");
        },
    });
};