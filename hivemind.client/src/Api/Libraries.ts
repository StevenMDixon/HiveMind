import type { Library } from '../Types/Library';
import type { EnumOptionsObj } from '../Types/General';
import { toEnumOptions } from "../Utilities/FormOptionsMapper"
import type { QuerySettingItem } from "../Types/Query";

export const fetchLibraries = async () => {
    const response = await fetch('/libraries');
    if (response.ok) {
        const data = await response.json();
        return data.libraries
    }
    return [];
};

export const fetchLibraryTypes = async (): Promise<EnumOptionsObj> => {
    const response = await fetch('/libraries/types');

    if (!response.ok) throw  new Error(`Failed to fetch library types: ${response.status} ${response.statusText}`);

    const data = await response.json();

    const options = data.types as QuerySettingItem[];

    return toEnumOptions(options, 'id', 'name');
};

export const createLibrary = async (libraryInputs: Library) => {
    const response = await fetch('/libraries', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ LibraryName: libraryInputs.libraryName, LibraryPath: libraryInputs.libraryPath, LibraryType: libraryInputs.libraryType }),
    });

    return response;
};

export const deleteLibrary = async (libraryId: number) => {
    const response = await fetch(`/libraries/${libraryId}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
    });

    return response;
};

export const fetchLibrary = async (id: string | undefined): Promise<Library> => {
    const response = await fetch('/libraries/' + id);

    if (!response.ok) {
        throw new Error(`Failed to fetch library: ${response.status} ${response.statusText}`);
    }

    const data = await response.json();
    return data;
};

export const saveLibrary = async (item: Library) => {
    const result = await fetch('/libraries/' + item.libraryId, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ LibraryName: item.libraryName, LibraryPath: item.libraryPath, LibraryType: item.libraryType }),
    });

    return result;
}