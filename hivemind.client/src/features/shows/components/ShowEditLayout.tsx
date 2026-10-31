import { Container } from "@mui/material"
import type { Show } from '../types';
import type { EnumOptionsObj } from '@/utilities/types'; 

import CustomForm from '../../../components/CustomForm';
import { type CustomFormField } from '../../../components/FormFields';
import { useShowUpdateHook } from '../hooks/showHooks';


interface ShowEditLayoutProps {
    show: Show,
    ratings: EnumOptionsObj
}

const ShowEditLayout = ({ show, ratings }: ShowEditLayoutProps) => {
    const { mutate: updateShow } = useShowUpdateHook();

    const handleSaveShow = async (show: Show) => {
        updateShow(show);
    }

    const fields = [
        { name: 'name', display: "Name", type: "Text", initialValue: show.name },
        { name: 'rating', display: "Rating", type: "Select", initialValue: show.rating, options: ratings }
    ] as CustomFormField[];

    return (
        <Container sx={{ mt: 5 }}>
            <CustomForm title="" save={handleSaveShow} initialValue={show} fields={fields} />
        </Container>
    )
}

export default ShowEditLayout;