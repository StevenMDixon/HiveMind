import { queryOptions } from '@tanstack/react-query';
import { getLibraries, getLibrary, getLibraryTypes } from '../api/requests';
import { libraryKeys } from './queryKeys';

export const libraryQueryOptions = (id: string) => {
    return queryOptions({
        queryKey: libraryKeys.detail(Number(id)),
        queryFn: () => getLibrary(id)
    });
}

export const librariesQueryOptions = () => {
    return queryOptions({
        queryKey: libraryKeys.all,
        queryFn: getLibraries
    });
}

export const libraryTypesQueryOptions = () => {
    return queryOptions({
        queryKey: ['libraryTypes'],
        queryFn: getLibraryTypes
    });
}