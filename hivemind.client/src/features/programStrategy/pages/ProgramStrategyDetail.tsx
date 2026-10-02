import { useParams } from "react-router-dom";
import { programStrategyDetailQueryOptions } from "../api/queryOptions";
import { useQuery } from '@tanstack/react-query';

import Container from '@mui/material/Container';
import Stack from '@mui/material/Stack';
// import { useNavigate } from "react-router-dom";

import Header from '@/components/Header';


// import EditingGrid, { type EditingGridColumns } from '@/components/EditingGrid';
import ProgramStrategyLayout from "../components/ProgramStrategyLayout";

const ProgramStrategyDetail = () => {
    const { id } = useParams();

    const { data: programStrategy } = useQuery(programStrategyDetailQueryOptions(Number(id ?? '0')));

    return (
        <Container disableGutters maxWidth={false}>
            <Header Title={`Program Strategy: ${id}`}>
            </Header>
            <Stack>
                <Container sx={{ mt: 5 }}>
                    {programStrategy && <ProgramStrategyLayout strategy={programStrategy} />}
                </Container>
            </Stack>
        </Container>
    )
}

export default ProgramStrategyDetail;