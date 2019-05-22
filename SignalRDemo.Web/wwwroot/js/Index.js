$(() => {

    const connection = new signalR.HubConnectionBuilder().withUrl("/jobHub").build();

    connection.start();

    $("#add-job").on('click', () => {

        const title = $("#title").val();
        connection.invoke("AddJob", title);
    });

    connection.on("NewJob", j => {

        $("#my-table").append(`<tr data-id="${j.id}"><td>${j.title}</td><td><button id="${j.id}" class="btn btn-primary btn-block do">I'll do it!</button></td></tr>`)
    });

    $("#my-table").on('click', ".do", function () {

        $(this).text('I\'m Done!');
        $(this).removeClass('btn-primary');
        $(this).addClass('btn-success');
        $(this).removeClass('do');
        $(this).addClass('done');
        const jobId = $(this).attr('id');
        connection.invoke("AddWorking", jobId);
    })

    connection.on("NewWorking", w => {

        $(`#${w.jobId}`).text(`${w.name} is doing it!`);
        $(`#${w.jobId}`).removeClass('btn-primary');
        $(`#${w.jobId}`).removeClass('do');
        $(`#${w.jobId}`).addClass('btn-warning');
    });

    $("#my-table").on('click', ".done", function () {

        $(this).parent().parent().remove();
        const jobId = $(this).attr('id');
        connection.invoke("Finished", jobId);
    })

    connection.on("FinishedJob", j => {

        $(`#${j.jobId}`).parent().parent().remove();
        
       });

   
})