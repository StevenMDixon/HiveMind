import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateTag, deleteTag, createTag } from '../api/requests';
import { useGlobalNotification } from '@/Dashboard/useGlobalNotification';

export const useTagCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createTag,
        onSuccess: () => {
            showNotification("Tag created", "success");
            queryClient.invalidateQueries({ queryKey: ['Tags'] });
        },
        onError: () => {
           showNotification("Failed to create tag", "error");
        },
    });
};


export const useTagUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateTag,
        onSuccess: () => {
            showNotification("Tag updated", "success");
            queryClient.invalidateQueries({ queryKey: ['Tags'] });
        },
        onError: () => {
           showNotification("Failed to update tag", "error");
        },
    });
};

export const useTagDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: deleteTag,
        onSuccess: () => {
            showNotification("Tag deleted", "success");
            queryClient.invalidateQueries({ queryKey: ['Tags'] });
        },
        onError: () => {
           showNotification("Failed to delete tag", "error");
        },
    });
};