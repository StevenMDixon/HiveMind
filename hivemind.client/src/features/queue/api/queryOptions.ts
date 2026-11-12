import { queryOptions } from '@tanstack/react-query';
import { getQueues, getMedia, getCategoryOptions, getCategory, getTags } from './requests';
import { queueKeys } from './queryKeys';

export const queueQueryOptions = () => {
    return queryOptions({
        queryKey: [...queueKeys.all],
        queryFn: getQueues
    });
}

export const mediaQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: [...queueKeys.all, id],
        queryFn: () => getMedia(id)
    });
}

export const tagCategoryOptions = () => {
    return queryOptions({
        queryKey: [...queueKeys.all, 'options'],
        queryFn: getCategoryOptions
    });
}

export const tagCategoryRequirementsOptions = () => {
    return queryOptions({
        queryKey: [...queueKeys.all, 'requirements'],
        queryFn: getCategory
    });
}

export const tagOptions = () => {
    return queryOptions({
        queryKey: ['tags'],
        queryFn: getTags
    }); 
}