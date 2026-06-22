import { queryOptions } from '@tanstack/react-query';
import { getSettings } from './requests';

export const settingsQueryOptions = () => {
    return queryOptions({
        queryKey: ['settings'],
        queryFn: getSettings
    });
}