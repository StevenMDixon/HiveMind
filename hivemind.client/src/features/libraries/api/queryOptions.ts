import { queryOptions } from '@tanstack/react-query';
import { getLibraries, getLibrary, getLibraryTypes, search } from '../api/requests';
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

export const libraryQuerySearchOptions = (searchTerm: string, page: number, count: number) => {
    return queryOptions({
        queryKey: ['libraryTypes', searchTerm, page, count],
        queryFn: () => search(searchTerm, page, count)
    });
}