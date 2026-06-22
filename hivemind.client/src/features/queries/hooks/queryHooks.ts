import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createQuery, deleteQuery, updateQuery } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { queryKeys } from '../api/queryKeys';

export const useQueryCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createQuery,
        onSuccess: () => {
            showNotification("Query created", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.all });
        },
        onError: () => {
            showNotification("Failed to create query", "error");
        },
    });
};

export const useQueryDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteQuery(id),
        onSuccess: () => {
            showNotification("Query deleted", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.details() });
        },
        onError: () => {
            showNotification("Failed to delete query", "error");
        },
    });
};

export const useQueryUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateQuery,
        onSuccess: () => {
            showNotification("Query updated", "success");
            queryClient.invalidateQueries({ queryKey: queryKeys.all });
        },
        onError: () => {
            showNotification("Failed to update query", "error");
        },
    });
};