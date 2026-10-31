import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createTransitions, deleteTransitions, updateTransitions } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { transitionKeys } from '../api/queryKeys';

export const useTransitionCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createTransitions,
        onSuccess: () => {
            showNotification("Transition created", "success");
            queryClient.invalidateQueries({ queryKey: transitionKeys.all });
        },
        onError: () => {
            showNotification("Failed to create transition", "error");
        },
    });
};

export const useTransitionDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteTransitions(id),
        onSuccess: () => {
            showNotification("Transition deleted", "success");
            queryClient.invalidateQueries({ queryKey: transitionKeys.all });
        },
        onError: () => {
            showNotification("Failed to delete transition", "error");
        },
    });
};

export const useTransitionUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateTransitions,
        onSuccess: () => {
            showNotification("Transition updated", "success");
            queryClient.invalidateQueries({ queryKey: transitionKeys.all });
        },
        onError: () => {
            showNotification("Failed to update transition", "error");
        },
    });
};