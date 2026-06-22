import { Container } from "@mui/material"
import type { Library } from '../types';
import type { EnumOptionsObj } from '@/utilities/types'; 

import CustomForm from '../../../components/CustomForm';
import { type CustomFormField } from '../../../components/FormFields';
import { useLibraryUpdateHook } from '../hooks/libraryHooks';


interface LibraryEditLayoutProps {
    library: Library
    libraryTypes: EnumOptionsObj
}

const LibraryEditLayout = ({ library, libraryTypes }: LibraryEditLayoutProps) => {
    const { mutate: updateLibrary } = useLibraryUpdateHook();

    const handleSaveLibrary = async (item: Library) => {
        updateLibrary(item);
    }

    const fields = [
        { name: 'libraryName', display: "Name", type: "Text", initialValue: library.libraryName },
        { name: 'libraryPath', display: "Path", type: "Text", initialValue: library.libraryPath },
        { name: 'pathsToIgnore', display: "Paths To Ignore", type: "Text", initialValue: library.pathsToIgnore },
        { name: 'libraryType', display: "Type", type: "Select", initialValue: library.libraryType, options: libraryTypes },
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="test" save={handleSaveLibrary} initialValue={library} fields={fields} />
        </Container>
    )
}

export default LibraryEditLayout;