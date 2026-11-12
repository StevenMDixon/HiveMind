import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateTags } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { queueKeys } from '../api/queryKeys';

export const useTagsUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateTags,
        onSuccess: () => {
            showNotification("Media Tags updated", "success");
            queryClient.invalidateQueries({ queryKey: queueKeys.all });
        },
        onError: () => {
            showNotification("Failed to udpate media tags", "error");
        },
    });
};