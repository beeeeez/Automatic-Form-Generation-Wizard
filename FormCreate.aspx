<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FormCreate.aspx.cs" Inherits="FormCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style>
        .contain {
            width: 80%;
            margin: auto;
            margin-top: 5%;
        }

        .btn {
            margin-right: 2%;
            margin-top: 2%;
        }

        #inner {
            width: 45%;
        }

        #newContent {
            width: 45%;
        }

        .form-control {
            width: 45%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="contain">
        <form action="SaveCreation.aspx" method="post"></form>
        <h3>New Form Creation -- <a href="Homepage.aspx">Return to Homepage</a></h3>
        <br />
        <div id="inner" runat="server">
            <h3>What is your new form's title? :</h3>
            <br />
            <input type="text" id="formtitle" placeholder="Enter a form title." class="form-control" />
            <div id="create"></div>
            <input type="hidden" id="totalQ" />
            <div class="btn btn-primary" id="addBtn">Add a New Question</div>
            <div class="btn btn-primary" id="addText">Add Text</div>
            <br />
            <input type="submit" value="Save Form" class="btn btn-primary" />
            <script>

                let qnum = 0;
                let ddlMem;
                $("#totalQ").val(qnum);


                $("#addBtn").click(function () {
                    qnum = parseInt($("#totalQ").val()) + 1;
                    let qDiv = '<div id="q' + qnum + '" class="question"><hr /><h4>Question #<span id="span' + qnum + '">' + qnum + '</span></h4><input type="text" id="tb' + qnum + '" placeholder="Untitled Question" class="form-control" style="width:40%;"><span id="del' + qnum + '" onclick="delQ(' + qnum + ')" class="btn btn-danger">Delete this Question</span><br /><label>What is the input type? :</label><br /><select id="ddl' + qnum + '" onchange="ddlChange(' + qnum + ')"  onfocus="ddlSave(' + qnum + ')"><option value="short">Short Text</option><option value="long">Long Text</option><option value="multiple">Multiple Choice</option><option value="checkbox">Checkboxes</option><option value="datetime">Date & Time</option></select><hr /></div>';
                    $("#totalQ").val(qnum);
                    $("#create").append(qDiv);

                });

                function delQ(qnum) {

                    $('#q' + qnum).remove();
                    reDraw(qnum);
                    $("#totalQ").val(parseInt($("#totalQ").val()) - 1);
                }

                function reDraw(hold) {
                    let qnum = parseInt(hold);
                    let totalNum = parseInt($("#totalQ").val());
                    let correctNum = qnum++;
                    while (qnum <= totalNum) {
                        $('#q' + qnum).attr('id', "q" + correctNum);
                        $('#tb' + qnum).attr('id', "tb" + correctNum);
                        $('#span' + qnum).html(correctNum);
                        $('#span' + qnum).attr('id', 'span' + correctNum);
                        $('#del' + qnum).attr('onclick', "delQ(" + correctNum + ")");
                        $('#del' + qnum).attr('id', "del" + correctNum);
                        $('#ddl' + qnum).attr('onchange', "ddlChange(" + correctNum + ")");
                        $('#ddl' + qnum).attr('onfocus', "ddlSave(" + correctNum + ")");
                        $('#ddl' + qnum).attr('id', "ddl" + correctNum);
                        qnum++;
                        correctNum++;
                    }

                }


                function ddlChange(hold) {
                    if ($('#ddl' + hold).val() == "multiple" || $('#ddl' + hold).val() == "checkbox") {
                        if ($('#options' + hold).length == 0) {
                            drawOption(hold);
                        }
                    }
                    else {
                        if ($('#options' + hold).length > 0) {
                            destroyOption(hold);
                        }
                    }
                }


                /*

                function ddlChange(hold) {
                    if ($('#ddl' + hold).val() == "multiple" || $('#ddl' + hold).val() == "checkbox") {
                        if (ddlMem != "multiple" && ddlMem != "checkbox") {
                            drawOption(hold);
                        }

                    }
                    else if ($('#ddl' + hold).val() != "multiple" && $('#ddl' + hold).val() != "checkbox") {
                        if (ddlMem == "multiple" || ddlMem == "checkbox") {
                            destroyOption(hold);
                        }
                    }


                }
                
                function ddlSave(hold) {
                    ddlMem = $('#ddl' + hold).val();
                }
                */

                function drawOption(hold) {
                    let draw = '<div id="options' + hold + '"><input type="hidden" id="q' + hold + 'OptionsTotal" value="2" /><input type="text" class="form-control" placeholder="Untitled Option" id="Option1" /><span id="delOption1" onclick="delOption(' + hold + ', 1)"  class="btn btn-danger">Delete this option </span><br /> <input type="text" class="form-control" placeholder="Untitled Option" id="Option2"><span id="delOption2" onclick="delOption(' + hold + ', 2)" class="btn btn-danger" >Delete this option </span><br /><span id="addOption' + hold + '" onclick="addOption(' + hold + ')" class="btn btn-primary" >Add option</span><hr /><br /></div> ';
                    $('#q' + hold).append(draw);
                }

                function addOption(hold) {
                    let opTotal = parseInt($('#q' + hold + 'OptionsTotal').val());
                    opTotal++;
                    let draw = '<input type="text" class="form-control" placeholder="Untitled Option" id="Option' + opTotal + '"><span id="delOption' + opTotal + '" onclick="delOption(' + hold + ', ' + opTotal + ')" class="btn btn-danger" >Delete this option </span>';
                    opTotal--;
                    $('#delOption' + opTotal).after(draw);
                }

                function destroyOption(hold) {
                    $('#options' + hold).remove();
                }

                function deleteOption(hold) {

                }

                /*
                function reDraw(qnum) {
                    while (qnum < parseInt($("#totalQ").val())) {
                        let pnum = qnum++;
                        $("#q" + pnum).attr("id", "q" + pnum);
                        $("#tb" + pnum).attr("id", "tb" + pnum);
                        $("#tb" + pnum).attr("placeholder", "Untitled Question " + pnum);
                        $("#del" + pnum).attr("onclick", "delQ(" + pnum + ")");
                        $("#ddl" + pnum).attr("onchange", "ddlChange(" + pnum + ")");
                        qnum++;
                    }
                    $("#totalQ").val(parseInt($("#totalQ").val())-1);
                }
                */

            </script>


        </div>
</asp:Content>

