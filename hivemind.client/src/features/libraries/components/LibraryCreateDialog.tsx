import { type CustomFormField } from '../../../components/FormFields';
import CustomDialog from '../../../components/Dialog';

import type { Library, EnumOptionsObj } from '../types';

interface NewLibraryFormProps {
    createLibrary: (a: Library) => void;
    libraryTypes?: EnumOptionsObj;
}

const LibraryCreateDialog = ({ createLibrary, libraryTypes }: NewLibraryFormProps) => {
    const libraryDefault = { id: 0, name: '', path: '', pathsToIgnore: '', type: 0, isProcessed: false };

    const fields = [
        { name: 'name', display: "Name", type: "Text", initialValue: libraryDefault.name, validator: (libraryName: string) => libraryName != '', required: true },
        { name: 'path', display: "Path", type: "Text", initialValue: libraryDefault.path, validator: (libraryPath: string) => libraryPath != '', required: true },
        { name: 'pathsToIgnore', display: "Paths to Ignore", type: "Text", initialValue: libraryDefault.pathsToIgnore },
        { name: 'type', display: "Type", type: "Select", initialValue: libraryDefault.type, required: true, options: libraryTypes }
    ] as CustomFormField[];

    return (
        <CustomDialog buttonText= "Add Library" title = "Create Library" save = { createLibrary } fields = { fields } initialValue = { libraryDefault } />
    )
}

export default LibraryCreateDialog;