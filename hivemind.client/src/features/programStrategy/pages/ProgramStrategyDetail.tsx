import { useParams } from "react-router-dom";
import { programStrategyDetailQueryOptions } from "../api/queryOptions";
import { useQuery } from '@tanstack/react-query';

import Container from '@mui/material/Container';
import Stack from '@mui/material/Stack';
// import { useNavigate } from "react-router-dom";
import CustomForm from "@/components/CustomForm";
import { type CustomFormField } from '@/components/FormFields';
import Header from '@/components/Header';

import { type ProgramStrategy } from '../types';

import { useProgramStrategyUpdateHook } from '../hooks/programStrategyHooks';

interface ProgramStrategyWrapperProps {
    strategy: ProgramStrategy;
}

const ProgramStrategyWrapper = ({ strategy }: ProgramStrategyWrapperProps) => {

    const { mutate: saveStrategy } = useProgramStrategyUpdateHook();

    const strategyFields = [
        { name: 'active', display: "Active", type: "Switch", initialValue: strategy.active },
        { name: 'name', display: "Name", type: "Text", initialValue: strategy.name },
        { name: 'advancedDays', display: "Days to Schedule", type: "Number", initialValue: strategy.advancedDays },
        { name: 'startDate', display: "Start Date", type: "Text", initialValue: strategy.startDate },
        { name: 'endDate', display: "End Date", type: "Text", initialValue: strategy.endDate }

    ] as CustomFormField[];

    const saveProgramStrategy = (e) => {
        console.log(e);
        saveStrategy(e);
    }

    return (
        <CustomForm title="Editing Program Strategy" save={saveProgramStrategy} initialValue={strategy} fields={strategyFields} />
    )
}

const ProgramStrategyDetail = () => {
    const { id } = useParams();

    const { data: programStrategy } = useQuery(programStrategyDetailQueryOptions(Number(id ?? '0')));

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title={`Program Strategy: ${id}`}>
            </Header>
            <Stack>
                <Container sx={{ maxWidth: "50%" }} >
                    {programStrategy && <ProgramStrategyWrapper strategy={programStrategy} />}
                </Container>
            </Stack>
        </Container>
    )
}

export default ProgramStrategyDetail;