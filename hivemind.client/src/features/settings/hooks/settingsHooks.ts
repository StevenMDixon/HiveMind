import { useMutation, useQueryClient } from '@tanstack/react-query';
import { updateSetting } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';

export const useSettingUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateSetting,
        onSuccess: () => {
            showNotification("Setting updated", "success");
            queryClient.invalidateQueries({ queryKey: ['settings'] });
        },
        onError: () => {
           showNotification("Failed to update setting", "error");
        },
    });
};