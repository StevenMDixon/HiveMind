// src/components/SignalRCacheSync.tsx
import { useCallback } from 'react';
import { useQueryClient } from '@tanstack/react-query';
import { useSignalREvent } from '../hooks/useSignalREvent';

export interface JobProgressDto {
  jobId: string;
  percent: number;
  status: string;
}

export interface JobCompletedDto {
  jobId: string;
  downloadUrl: string;
}

export function SignalRCacheSync() {
  const queryClient = useQueryClient();

  // 1. High-frequency progress updates -> Patch cache directly (no HTTP refetch)
  const handleJobProgress = useCallback(
    (progress: JobProgressDto) => {
      queryClient.setQueryData<JobProgressDto>(
        ['jobs', progress.jobId, 'progress'],
        progress
      );
    },
    [queryClient]
  );

  // 2. Final completion event -> Invalidate list/detail queries so TanStack refetches fresh DB state
  const handleJobCompleted = useCallback(
    (result: JobCompletedDto) => {
      queryClient.invalidateQueries({ queryKey: ['jobs'] });
      queryClient.invalidateQueries({ queryKey: ['jobs', result.jobId] });
    },
    [queryClient]
  );

  useSignalREvent<JobProgressDto>('JobProgressUpdated', handleJobProgress);
  useSignalREvent<JobCompletedDto>('JobCompleted', handleJobCompleted);

  return null;
}