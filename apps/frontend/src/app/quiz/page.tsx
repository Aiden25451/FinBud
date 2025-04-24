import Loading from '@/_components/loader/loading';
import QuizPage from '@/_components/Quiz-Component/quiz';
import { getHistoryFunction } from '@/_utils/quiz-functions';
import { getSession } from '@auth0/nextjs-auth0';
import { Suspense } from 'react';

export default async function Page() {
  const session = await getSession();

  if (session) {
    const data = await getHistoryFunction();
    return <Suspense fallback={<Loading />}>
      <QuizPage data={data}></QuizPage>;
    </Suspense>
  }

  return (
    <Suspense fallback={<Loading />}>
      <QuizPage
        data={{
          loading: false,
          historyArray: [0],
          error: 'Not logged in',
          initialState: false,
        }}
      ></QuizPage>
    </Suspense>
  );
}
