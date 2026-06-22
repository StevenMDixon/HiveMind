import { type CustomFormField } from '../../../components/FormFields';
import CustomDialog from '../../../components/Dialog';

import type { Library, EnumOptionsObj } from '../types';

interface NewLibraryFormProps {
    createLibrary: (a: Library) => void;
    libraryTypes?: EnumOptionsObj;
}

const LibraryCreateDialog = ({ createLibrary, libraryTypes }: NewLibraryFormProps) => {
    const libraryDefault = { libraryId: 0, libraryName: '', libraryPath: '', pathsToIgnore: '', libraryType: 0, isProcessed: false };

    const fields = [
        { name: 'libraryName', display: "Name", type: "Text", initialValue: libraryDefault.libraryName, validator: (libraryName: string) => libraryName != '', required: true },
        { name: 'libraryPath', display: "Path", type: "Text", initialValue: libraryDefault.libraryPath, validator: (libraryPath: string) => libraryPath != '', required: true },
        { name: 'pathsToIgnore', display: "Paths to Ignore", type: "Text", initialValue: libraryDefault.pathsToIgnore },
        { name: 'libraryType', display: "Type", type: "Select", initialValue: libraryDefault.libraryType, required: true, options: libraryTypes }
    ] as CustomFormField[];

    return (
        <CustomDialog buttonText= "Add Library" title = "Create Library" save = { createLibrary } fields = { fields } initialValue = { libraryDefault } />
    )
}

export default LibraryCreateDialog;