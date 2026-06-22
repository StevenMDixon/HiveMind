import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createLibrary, deleteLibrary, updateLibrary, reprocessLibrary } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { libraryKeys } from '../api/queryKeys';

export const useLibraryCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createLibrary,
        onSuccess: () => {
            showNotification("Library created", "success");
            queryClient.invalidateQueries({ queryKey: libraryKeys.all });
        },
        onError: () => {
            showNotification("Failed to create library", "error");
        },
    });
};

export const useLibraryDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteLibrary(id),
        onSuccess: () => {
            showNotification("Library deleted", "success");
            queryClient.invalidateQueries({ queryKey: libraryKeys.all });
        },
        onError: () => {
            showNotification("Failed to delete library", "error");
        },
    });
};

export const useLibraryUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateLibrary,
        onSuccess: () => {
            showNotification("Library updated", "success");
            queryClient.invalidateQueries({ queryKey: libraryKeys.all });
        },
        onError: () => {
            showNotification("Failed to update library", "error");
        },
    });
};

export const useLibraryRefreshHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: reprocessLibrary,
        onSuccess: () => {
            showNotification("Library marked for reprocessing", "success");
            queryClient.invalidateQueries({ queryKey: libraryKeys.all });
        },
        onError: () => {
            showNotification("Failed to reprocess library", "error");
        },
    });
};