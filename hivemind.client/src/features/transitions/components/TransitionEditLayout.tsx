import { Container } from "@mui/material"
import type { Transition } from '../types';

import CustomForm from '@/components/CustomForm';
import { fields } from '../fields';

import { useTransitionUpdateHook } from '../hooks/transitionHooks';

interface TransitionEditLayoutProps {
    transitionData: Transition
}

const TransitionEditLayout = ({ transitionData }: TransitionEditLayoutProps) => {

    const { mutate: updateTransition } = useTransitionUpdateHook();

    const handleSaveTransition = async (item: Transition) => {
        updateTransition(item);
    }

    return (
        <Container sx={{ mt: 5 }}>
            {
                transitionData &&
                <CustomForm title={transitionData.name} save={handleSaveTransition} initialValue={transitionData} fields={fields} />
            }
        </Container>
    )
}

export default TransitionEditLayout;