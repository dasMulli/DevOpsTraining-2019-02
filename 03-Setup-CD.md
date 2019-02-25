## Lab 3: Set up continuous deployment to Azure

1. Make sure you have an Azure Subscription set up.

If you have an MSDN (e.g. Visual Studio Professional) subscription, you can use this one.
If you don't have an Azure Subscription, work together as teams with a person that has a subscription.
You can also use a trial offer if available.

2. Create a Resource Group for your applications:

Use the add resource group button to create a new resource group:

![add resource group](images/03-01-add-resource-group.png)

![resource group wizard 1](images/03-02-create-resource-group-wizard-1.png)

![resource group wizard 1](images/03-03-create-resource-group-wizard-2.png)

It may take a few seconds to minutes until the resource group is ready to use. You can use the refresh button to make the new group appear after deployment.

![open resource group](images/03-04-open-resource-group.png)

Now let's add a new web app to the resource group:

![add to resource group](images/03-05-rg-add.png)

![add web app 1](images/03-06-add-web-app-1.png)

![add web app 2](images/03-07-add-web-app-1.png.png)

You will need to find a unique name for your web app.
Be sure to:
* Use your VS subscription if possbile (we shouldn't be creating resources that result in costs though as long as you turn them off afterwards)
* Use Windows as a platform for the Web App (the free shared infrasturcture tier is only avilable on windows. Otherwise, Linux should work just as well)
* Create a new Web App Plan so that you can easily remove everything afterwards and don't affect any production apps.

![web app and plan config](images/03-08-web-app-and-plan-config.png)

![use free tier](images/03-09-free-tier.png)

![commit plan config](images/03-10-commit-plan-config.png)

![review and create web app](images/03-11-create-web-app.png)

After a few minutes, your web app should be ready to use. You will recieve a notification when the deployment has finished.

![deployment notifications](images/03-12-deployment-notifications.png)

3. Configure a release pipeline in Azure DevOps to deploy to the web application

![create release pipeline](images/03-13-create-release-pipeline.png)

For simplicity, start with an empty job template:

![empty job](images/03-14-empty-job-template.png)

Name the first and only environment "Production".
 - Because we don't always test our apps; but when we do, we do it in Production ;)

![production env](images/03-15-production-env.png)

Then set up the release pipeline to use the previously configured build pipeline as artifact:

![add build artifact](images/03-16-add-build-artifact.png)

Configure this build artifact to be a continuous deployment trigger:

![continuous deployment](images/03-16-enable-continuous-deployment.png)

Click on the "0 tasks" link in the environment to jump directoy to the environment's task configuration:

![tasks](images/03-16-tasks.png)

Add an "Azure Web App" task to the agent steps:

![add web app task](images/03-17-add-web-app-task.png)

Select your azure subscription that you have used to create the web app. You may need to authorize Azure DevOps to use this subscription by clicking "Authorize" and following any wizard steps (OAuth2 login):

![select subscription](images/03-18-select-subscription.png)

After the authorization step you can select the web app you want to deploy to:

![select web app](images/03-19-select-web-app.png)

This is all we need to configure for the continuous deploy. Change the name for the release pipeline and save it:

![give name and save release pipeline](images/03-20-give-name-and-save.png)

![save release pipeline](images/03-21-save-release-pipeline.png)

Now head to the latest build and release it:

![release latest build](images/03-22-release-latest-build.png)

![create release for build](images/03-23-create-release-for-build.png)

You can navigate to the release by clicking the link Azure DevOps will show you at this point:

![navigate to release](images/03-24-navigate-to-release.png)

![Release 1](images/03-25-release-1.png)

The release should succeed in a matter of minutes at maximum. Review the release logs if it should fail.

You can now visit the public URL of the web app you created to test your site "in Production":

![Browse live page](images/03-26-browse-live-page.png)

![Browse live hello world](images/03-27-browse-live-hello-world.png)

4. Change "Hello World" to "Hello DevOps!" using only the Web UI

In the next steps you can use your CI/CD pipeline to deploy a simple change by only using the Web UI of Azure DevOps. In our case, we want to change the "Hello World" text that the app renders for any unknown URL.

So let's start by editing `Startup.cs`:

![edit Startup.cs](images/03-28-edit-startup.png)

Change the text to "Hello DevOps!", commit the change to a new feature branch and create a pull request:

![change hello text and commit](images/03-29-change-and-commit.png)

![commit hello to branch](images/03-30-commit-hello-to-branch.png)

![create hello text PR](images/03-31-create-hello-pr.png)

But we now broke our integration test. If you let the build on the PR run, it will fail. So we need to update the integration test as well. The branch policy will forbid us to merge any code that makes the tests fail.

Be sure to commit the test changes to the branch that you have used to create the pull request!

![edit integration test](images/03-32-fix-test.png)

![commit integration test fix](images/03-33-commit-test-fix.png)

Whenever you use the web UI to commit to a branch that has an open pull request, Azure DevOps will show you the link to it for easy navigation:

![navigate to pr](images/03-34-navigate-to-pr.png)

Let's try the "auto complete" feature on pull requests. This lets us specify that the pr should be immediately merged once all policies succeed.

This is useful when a code is already reviewed and you just fixed tests and don't want to wait for a build to finish so you can click "Complete".

![set pr to auto complete](images/03-35-set-pr-to-auto-complete.png)

![pr auto complete options](images/03-36-autocomplete-options.png)

You can now approve the PR. If the build has already run, this will complete the PR. If not, you can go for a coffee or pizza and let the pipelines run.

![approve hello text pr](images/03-37-approve-hello-pr.png)

After the pr has merged, a new CI bulid of the master branch will be triggered:

![new hello text ci build](images/03-38-hello-ci-build.png)

once that build has finished, a new deployment will automatically be created and the deplyoment to production starged.

You can monitor this via the release pipeline overview:

![hello text deployment](images/03-39-hello-deployment.png)

When the deplyoment is complete, you can see the result on the live webiste:

![new hello text live](images/03-40-new-hello-text-live.png)

Congratulations, you set up a CI/CD pipeline for a production app!

# Where to go from here?

I suggest you play around with the deployment options, read about "deployment slots" on Azure Web Apps that allow for zero-downtime deployments and try out deploying to multiple environments consequtively (e.g. requiring approvement from specific people)
