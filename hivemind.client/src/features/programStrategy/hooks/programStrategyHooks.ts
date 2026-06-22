import { useMutation, useQueryClient } from '@tanstack/react-query';
import { createProgramStrategy , deleteProgramStrategy, updateProgramStrategy } from '../api/requests';
import { useGlobalNotification } from '@/dashboard/useGlobalNotification';
import { strategyKeys } from '../api/queryKeys';

export const useProgramStrategyCreateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: createProgramStrategy,
        onSuccess: () => {
            showNotification("Program Strategy created", "success");
            queryClient.invalidateQueries({ queryKey: strategyKeys.all });
        },
        onError: () => {
            showNotification("Failed to create program strategy", "error");
        },
    });
};

export const useProgramStrategyDeleteHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: (id: number) => deleteProgramStrategy(id),
        onSuccess: () => {
            showNotification("Program strategy deleted", "success");
            queryClient.invalidateQueries({ queryKey: strategyKeys.all });
        },
        onError: () => {
            showNotification("Failed to delete program strategy", "error");
        },
    });
};

export const useProgramStrategyUpdateHook = () => {
    const queryClient = useQueryClient();
    const { showNotification } = useGlobalNotification();

    return useMutation({
        mutationFn: updateProgramStrategy,
        onSuccess: () => {
            showNotification("Program strategy updated", "success");
            queryClient.invalidateQueries({ queryKey: strategyKeys.all });
        },
        onError: () => {
            showNotification("Failed to update program strategy", "error");
        },
    });
}